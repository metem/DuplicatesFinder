import { Component, OnInit } from '@angular/core';
import { HubConnection } from '@aspnet/signalr-client';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
    private hubConnection: HubConnection;
    messages: string[] = [];

    ngOnInit() {
        this.hubConnection = new HubConnection('/DuplicatesFinderHub');

        this.hubConnection
            .start()
            .then(() => console.log('Connection started!'))
            .catch(err => console.log('Error while establishing connection :('));

        this.hubConnection.on('DuplicateFound',
            (data: any) => {
                this.messages.push('Duplicate: ' + data.duplicate.name + ' original: ' + data.original.name);
            });
    }
}