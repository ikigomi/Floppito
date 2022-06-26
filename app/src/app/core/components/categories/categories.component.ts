import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/models/Category/Category';
import { CategoryService } from '../../services/category.service';
import {Guid} from 'guid-typescript'

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss']
})
export class CategoriesComponent implements OnInit {
  constructor() { }

  ngOnInit(): void {

  }

}
