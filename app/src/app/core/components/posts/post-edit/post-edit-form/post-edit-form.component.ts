import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Post } from 'src/app/models/Post/Post';
import { Guid } from 'guid-typescript'
import { Category } from 'src/app/models/Category/Category';
import { Observable } from 'rxjs';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { ImageService } from 'src/app/core/services/image.service';
import { Schedule } from 'src/app/models/Enums/Schedule';
import { Experience } from 'src/app/models/Enums/Experience';
import { MoneyCode } from 'src/app/models/Enums/MoneyCode';

@Component({
  selector: 'app-post-edit-form',
  templateUrl: './post-edit-form.component.html',
  styleUrls: ['./post-edit-form.component.scss']
})
export class PostEditFormComponent implements OnInit {

  @Output() createEvent = new EventEmitter();
  @Output() editEvent = new EventEmitter();
  @Output() fileUploadEvent = new EventEmitter();

  @Input() post$?: Observable<Post>;
  @Input() categories?: Category[];

  editForm: FormGroup;
  file: any;
  post?: Post;
  imageUrls: SafeUrl[] = [];
  filesToUpload: File[] = [];
  a = 0;
  

  scheduleArr: Schedule[] = [];
  experienceArr: Experience[] = [];
  salaryFromRange: [number, number] = [0, 300000];
  salaryToRange: [number, number] = [50000, 500000];
  salaryFrom: number = 0;
  salaryTo: number = 50000;
  HideSalaryFrom = false;
  HideSalaryTo = false;

  constructor(private _fb: FormBuilder,
    private _imageService: ImageService) {
    this.editForm = this._fb.group({
      'id': [],
      'title': ['', Validators.required],
      'description': [''],
      'author': [],
      'categoryId': ['', Validators.required],
      'salaryFrom': [this.salaryFromRange[0]],
      'salaryTo': [this.salaryToRange[0]],
      'workExperience': [],
      'workSchedule': [],
      'moneyCode': [MoneyCode.RUB],
    })
  }

  nextPage() {
    this.a = ++this.a % this.imageUrls.length;
  }

  previousPage() {

    this.a = (--this.a + this.imageUrls.length) % this.imageUrls.length;
  }

  ngOnInit(): void {
    this.post$?.subscribe(res => {
      this.post = res;
      this.imageUrls = this.post.images;
      this.editForm.patchValue(res);

    })
  }

  get titleControl() {
    return this.editForm.get('title');
  }

  get categoryControl() {
    return this.editForm.get('categoryId');
  }


  submit() {
    
    if (this.editForm.invalid || this.salaryFrom > this.salaryTo && !this.HideSalaryFrom && !this.HideSalaryTo) {
      this.editForm.markAllAsTouched();
      return;
    }

    if (this.experienceArr.length)
      this.editForm.get('workExperience')?.setValue(this.experienceArr.map(x => this.experienceKeys().indexOf(x)));

    if (this.scheduleArr.length)
      this.editForm.get('workSchedule')?.setValue(this.scheduleArr.map(x => this.scheduleKeys().indexOf(x)));

    if (this.HideSalaryFrom) {
      this.editForm.get('salaryFrom')?.setValue(null);
    }

    if (this.HideSalaryTo) {
      this.editForm.get('salaryTo')?.setValue(null);
    }

    this.editForm.get('moneyCode')?.setValue(this.moneyCodeKeys().indexOf(this.editForm.get('moneyCode')?.value));

    if (this.post) {
      let editedPost: Post = this.editForm.value;
      editedPost.images = this.post.images;

      this.editEvent.next(this.editForm.value);
      return;
    }


    this.fileUploadEvent.next(this.filesToUpload);
    this.editForm.get('id')?.setValue(Guid.createEmpty().toString());

    this.createEvent.next(this.editForm.value);
    
    if (this.HideSalaryFrom) {
      this.editForm.get('salaryFrom')?.setValue(this.salaryFromRange[0]);
    }

    if (this.HideSalaryTo) {
      this.editForm.get('salaryTo')?.setValue(this.salaryToRange[0]);
    }
  }

  handleFileInput(event: any) {
    this.imageUrls = [];
    this.filesToUpload = [];
    const files = (<HTMLInputElement>event.target).files!;

    const reader: FileReader[] = [];

    for (let index = 0; index < files?.length; index++) {

      if (!files[index]?.type.includes("image"))
        continue;

      this.filesToUpload.push(files?.item(index)!);

      reader.push(new FileReader());
      reader[index].readAsDataURL(files?.item(index)!);

      files?.item(index)?.arrayBuffer().then(res => {
        const stringUrl = this.toBlobUrl(res);
        const safeUrl = this._imageService.removeUnsafe(stringUrl);
        this.imageUrls.push(safeUrl);
      });

    }

  }

  toBlobUrl(buffer: ArrayBuffer): string {
    let arrayBufferView = new Uint8Array(buffer);
    const blob = new Blob([arrayBufferView], { type: "image/jpg;base64" });
    const urlCreator = window.URL || window.webkitURL;
    return urlCreator.createObjectURL(blob);
  }

  formatLabel(value: number) {
    if (value >= 1000) {
      return Math.round(value / 1000) + 'k';
    }


    return value;
  }

  experienceKeys(): Array<any> {
    let keys = Object.values(Experience);
    return keys.filter(x => keys.indexOf(x) % 2 == 0);

  }

  scheduleKeys(): Array<any> {
    let keys = Object.values(Schedule);
    return keys.filter(x => keys.indexOf(x) % 2 == 0);

  }

  moneyCodeKeys(): Array<any> {
    let keys = Object.values(MoneyCode);  
    return keys.filter(x => keys.indexOf(x) % 2 == 0);

  }

  toggleWorkExperience(checkbox: any) {
    if (checkbox.checked && !this.experienceArr.includes(checkbox.source.value))
      this.experienceArr.push(checkbox.source.value);
    else if (!checkbox.checked)
      this.experienceArr.splice(this.experienceArr.indexOf(checkbox.source.value), 1);
  }

  toggleWorkSchedule(checkbox: any) {
    if (checkbox.checked && !this.scheduleArr.includes(checkbox.source.value))
      this.scheduleArr.push(checkbox.source.value);
    else if (!checkbox.checked)
      this.scheduleArr.splice(this.scheduleArr.indexOf(checkbox.source.value), 1);
  }

}
