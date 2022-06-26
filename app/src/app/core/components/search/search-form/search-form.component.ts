import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { LoginFormComponent } from 'src/app/login/login/login-form/login-form.component';
import { Experience } from 'src/app/models/Enums/Experience';
import { Schedule } from 'src/app/models/Enums/Schedule';
import { SearchIn } from 'src/app/models/Enums/SearchIn';
import { Sorting } from 'src/app/models/Enums/Sorting';

@Component({
  selector: 'app-search-form',
  templateUrl: './search-form.component.html',
  styleUrls: ['./search-form.component.scss']
})
export class SearchFormComponent implements OnInit {

  @Output() searchEvent = new EventEmitter();


  searchForm: FormGroup;
  sorting: Sorting = Sorting.ByTitle;
  scheduleArr: Schedule[] = [];
  experienceArr: Experience[] = [];
  searchIn: SearchIn = SearchIn.Title;
  shouldBeChecked = true;

  constructor(private _fb: FormBuilder) {
    this.searchForm = this._fb.group({
      'searchString': [],
      'searchIn': [this.searchIn],
      'salaryFrom': [],
      'workExperience': [],
      'workSchedule': [],
      'sortBy': [this.sorting],
    })
  }

  ngOnInit(): void {
  }

  get searchStringControl() {
    return this.searchForm.get('searchString');
  }

  get salaryFromControl() {
    return this.searchForm.get('salaryFrom');
  }

  get searchInControl() {
    return this.searchForm.get('searchIn');
  }

  get workExperienceControl() {
    return this.searchForm.get('workExperience');
  }

  get workScheduleControl() {
    return this.searchForm.get('workSchedule');
  }

  get sortByControl() {
    return this.searchForm.get('sortBy');
  }

  submit() {
    if (this.searchForm.invalid) {
      this.searchForm.markAllAsTouched();
      return;
    }

    if (this.experienceArr.length)
      this.searchForm.get('workExperience')?.setValue(this.experienceArr.map(x => this.experienceKeys().indexOf(x)));

    if (this.scheduleArr.length)
      this.searchForm.get('workSchedule')?.setValue(this.scheduleArr.map(x => this.scheduleKeys().indexOf(x)));

    if (this.searchIn)
      this.searchForm.get('searchIn')?.setValue(this.searchInKeys().indexOf(this.searchIn));

    if (this.sorting)
      this.searchForm.get('sortBy')?.setValue(this.sortingKeys().indexOf(this.sorting));

    this.searchEvent.next(this.searchForm.value);

  }

  searchInKeys(): Array<any> {
    let keys = Object.values(SearchIn);
    return keys.filter(x => keys.indexOf(x) % 2 == 0);
  }

  experienceKeys(): Array<any> {
    let keys = Object.values(Experience);
    return keys.filter(x => keys.indexOf(x) % 2 == 0);

  }

  scheduleKeys(): Array<any> {
    let keys = Object.values(Schedule);
    return keys.filter(x => keys.indexOf(x) % 2 == 0);

  }

  sortingKeys(): Array<any> {
    let keys = Object.values(Sorting);
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
