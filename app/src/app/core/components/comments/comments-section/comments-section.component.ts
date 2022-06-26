import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { CommentService } from 'src/app/core/services/comment.service';
import { UserStorageService } from 'src/app/core/services/user-storage.service';
import { Comment } from 'src/app/models/Comment/Comment';
import { Guid } from "guid-typescript";
import { User } from 'src/app/models/User/User';

@Component({
  selector: 'app-comments-section',
  templateUrl: './comments-section.component.html',
  styleUrls: ['./comments-section.component.scss']
})
export class CommentsSectionComponent implements OnInit {
  @ViewChild('newCommentBody') commentBody:any;

  @Input() comments?:Comment[];
  @Input() author?:User;

  @Output() onDelete = new EventEmitter();
  @Output() onAdd = new EventEmitter();
  @Output() onEdit = new EventEmitter();

  isEditing:boolean=false;
  constructor(public _userStorage:UserStorageService) { }

  ngOnInit(): void {

  }

  delete(id:string) {
    this.onDelete.next(id);
  }

  add(body:string) {
    this.commentBody.nativeElement.value = "";
    this.onAdd.next(body);
  }

  edit(body:string) {
    this.isEditing = false;
    this.onEdit.next(body);
  }
}
