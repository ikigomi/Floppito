import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Observable } from 'rxjs';
import { Category } from 'src/app/models/Category/Category';

@Component({
  selector: 'app-category-table',
  templateUrl: './category-table.component.html',
  styleUrls: ['./category-table.component.scss']
})
export class CategoryTableComponent implements OnInit {

  @Input() categories$?:Observable<Category[]>;

  @Output() addEvent = new EventEmitter();
  @Output() deleteEvent = new EventEmitter();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  data!:MatTableDataSource<Category>;
  displayedColumns: string[] = ['select', 'title'];
  selection = new SelectionModel<Category>(true, []); //multiply selection and initial values

  constructor() { }

  ngOnInit(): void {
    this.categories$?.subscribe(res => {
      this.data = new MatTableDataSource<Category>(res);
      this.data.paginator = this.paginator;
      this.data.sort = this.sort;   
    })
  }

  add(title:string) {
    //TODO:Локальный отклик
    this.addEvent.next(title);
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
