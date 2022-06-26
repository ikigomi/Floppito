import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FilterModel } from 'src/app/models/FilterModel';
import { PostService } from '../../services/post.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {

  constructor(private _postService: PostService,
              private _router: Router,) { }

  ngOnInit(): void {
  }

  search(filter:FilterModel) {    
    let queryParams = filter
    this._router.navigate(['/posts/'], { queryParams });
  }

}
