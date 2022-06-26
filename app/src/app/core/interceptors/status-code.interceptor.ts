import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry, tap } from 'rxjs/operators';
import { NotificationService } from '../services/notification.service';

@Injectable()
export class StatusCodeInterceptor implements HttpInterceptor {

  constructor(private _notificationService: NotificationService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      tap((event) => {
        if (event instanceof HttpResponse) {          
          let message = ""

          //201-300
          if (event.status > 200 && event.status < 300) {
            switch (event.status) {
              case 201:
                message = "Успешно создано";
                break;
              case 204:
                message = "Успешно обновлено";
                break;
            }
            this._notificationService.showSuccess(message, event.status.toString());
          }
        }
      }),

      catchError((err) => {
        let message = ""
        //400+
        if (err.status >= 400) {
          switch (err.status) {
            case 400:
              message = "Произошла ошибка. Проверьте корректность введенных данных";
              break;
            case 401:
              message = "Доступ разрешен только авторизованным пользователям";
              break;
            case 403:
              message = "Доступ запрещен";
              break;
            case 404:
              message = "Страница не найдена";
              break;
            case 500:
              message = "Произошла ошибка сервера.";
              break;
          }
          this._notificationService.showError(message, err.status.toString());
        }

        return throwError(err);
      }));
  }
}
