import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Observable } from 'rxjs';
import { Post } from 'src/app/models/Post/Post';

@Component({
  selector: 'app-posts-list',
  templateUrl: './posts-list.component.html',
  styleUrls: ['./posts-list.component.scss']
})
export class PostsListComponent implements OnInit {
  @Input() posts$?: Observable<Post[]>;
  @Input() posts?: Post[];

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  data!:MatTableDataSource<Post>;

  constructor() { }

  ngOnInit(): void {
    this.posts$?.subscribe(res => {
      this.posts = res;
      this.data = new MatTableDataSource<Post>(res);
      this.data.paginator = this.paginator;
      this.posts$=this.data.connect();
    })

  }

}
