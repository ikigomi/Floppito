import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Observable } from 'rxjs';
import { ImageService } from 'src/app/core/services/image.service';
import { Comment } from 'src/app/models/Comment/Comment';

@Component({
  selector: 'app-comment-table',
  templateUrl: './comment-table.component.html',
  styleUrls: ['./comment-table.component.scss']
})
export class CommentTableComponent implements OnInit {

  @Input() comments$?:Observable<Comment[]>;

  @Output() addEvent = new EventEmitter();
  @Output() deleteEvent = new EventEmitter();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  data!:MatTableDataSource<Comment>;
  displayedColumns: string[] = ['select', 'authorUserName', 'body', 'details'];
  selection = new SelectionModel<Comment>(true, []); //multiply selection and initial values

  constructor(private _imageService:ImageService) { }

  ngOnInit(): void {
    this.comments$?.subscribe(res => {
      res.map(x=> x.author.avatarUrl = this._imageService.fromBase64ToUrl(x.author.avatar))
      
      this.data = new MatTableDataSource<Comment>(res);
      this.data.paginator = this.paginator;
      this.data.sort = this.sort;   
    })
  }

  delete() {
    const ids = this.selection.selected.map(c=>c.id);
    this.deleteEvent.next(ids);
  
    this.data.data = this.data.data.filter(p => !ids.includes(p.id));
    this.selection.clear();
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.data.data.length;
    return numSelected == numRows;
  }
  
  masterToggle() {
    this.isAllSelected() ?
        this.selection.clear() :
        this.data.data.forEach(row => this.selection.select(row));
  }

  applyFilter(filterEvent:KeyboardEvent) {    
    this.data.filter = (filterEvent.target as HTMLTextAreaElement).value.trim().toLowerCase();
  }

}
