import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  private _hubConnection?: HubConnection

  public ConfigureService() {
    this._hubConnection = new HubConnectionBuilder()
      .withUrl(environment.url + 'chat')
      .build();
  }

  public GetHubConnection(){
    return this._hubConnection;
  }
}
