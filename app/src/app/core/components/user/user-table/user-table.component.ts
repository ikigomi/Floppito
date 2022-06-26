import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Observable } from 'rxjs';
import { Roles } from 'src/app/models/Enums/Roles';
import { User } from 'src/app/models/User/User';

@Component({
  selector: 'app-user-table',
  templateUrl: './user-table.component.html',
  styleUrls: ['./user-table.component.scss']
})
export class UserTableComponent implements OnInit {

  @Input() users$?:Observable<User[]>;
  @Output() deleteAdminEvent = new EventEmitter();
  @Output() addAdminEvent = new EventEmitter();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  data!:MatTableDataSource<User>;
  displayedColumns: string[] = ['select', 'userName', 'name', 'gender', 'birthDate', 'roles','details'];
  selection = new SelectionModel<User>(true, []); //multiply selection and initial values

  constructor() { }

  ngOnInit(): void {
    this.users$?.subscribe(res => {
      this.data = new MatTableDataSource<User>(res);
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

  addAdmins() {
    this.addAdminEvent.next(this.selection.selected);
    //TODO:локальный отклик
    this.selection.clear();
  }

  deleteAdmins() {
    this.deleteAdminEvent.next(this.selection.selected);
    //TODO:локальный отклик
    this.selection.clear();
  }
}
