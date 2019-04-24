import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})

export class SignalRService {
    public observation: BehaviorSubject<string>;
    private hubConnection: signalR.HubConnection;

    constructor() {
        this.observation = new BehaviorSubject<string>('No received observations.');
    }

    public startConnection = () => {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .configureLogging(signalR.LogLevel.Debug)
            .withUrl('https://localhost:44335/observation', {
                skipNegotiation: true,
                transport: signalR.HttpTransportType.WebSockets
            })
            .build();

        this.hubConnection
            .start()
            .then(() => console.log('Connection started'))
            .catch(err => console.log('Error while starting connection: ' + err))
    }

    public addObservationListener = () => {
        this.hubConnection.on('NewObservation', (observation) => {
            this.observation.next(observation);
            console.log(observation);
        });
    }
}
