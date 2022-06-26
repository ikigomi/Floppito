import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Observable } from 'rxjs';
import { Post } from 'src/app/models/Post/Post';

@Component({
  selector: 'app-posts-table',
  templateUrl: './posts-table.component.html',
  styleUrls: ['./posts-table.component.scss']
})
export class PostsTableComponent implements OnInit {
  @Input() posts$?:Observable<Post[]>;
  @Input() showRejectButton:boolean = true;
  @Input() showAcceptButton:boolean = true;
  @Output() acceptEvent = new EventEmitter();
  @Output() rejectEvent = new EventEmitter();
  @Output() deleteEvent = new EventEmitter();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  data!:MatTableDataSource<Post>;
  displayedColumns: string[] = ['select', 'title', 'description', 'creatorUserName', 'category', 'details'];
  selection = new SelectionModel<Post>(true, []); //multiply selection and initial values

  constructor() { }

  ngOnInit(): void {
    this.posts$?.subscribe(res => {
      this.data = new MatTableDataSource<Post>(res);
      this.data.paginator = this.paginator;
      this.data.sort = this.sort;
    })
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

  acceptPosts() {
    const ids = this.selection.selected.map(c=>c.id);
    this.acceptEvent.next(ids);
    this.data.data = this.data.data.filter(p => !this.selection.selected.includes(p)); //удаляем локально элементы для отклика
    this.selection.clear();
  }

  rejectPosts() {
    const ids = this.selection.selected.map(c=>c.id);
    this.rejectEvent.next(ids);
    this.data.data = this.data.data.filter(p => !this.selection.selected.includes(p)); //удаляем локально элементы для отклика
    this.selection.clear();
  }

  deletePosts() {
    const ids = this.selection.selected.map(c=>c.id);
    this.deleteEvent.next(ids);
  
    this.data.data = this.data.data.filter(p => !ids.includes(p.id));
    this.selection.clear();
  }
}
