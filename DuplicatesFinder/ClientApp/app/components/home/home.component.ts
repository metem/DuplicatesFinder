import { Component, OnInit } from '@angular/core';
import { HubConnection } from '@aspnet/signalr-client';
import { HttpHeaders, HttpClient } from '@angular/common/http';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
    path: string;
    messages: string[] = [];

    private headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    private hubConnection: HubConnection;

    constructor(private http: HttpClient) {
    }

    ngOnInit() {
        this.hubConnection = new HubConnection('/DuplicatesFinderHub');

        this.hubConnection
            .start()
            .then(() => console.log('Connection started'))
            .catch(err => console.log('Error while establishing connection'));

        this.hubConnection.on('JobStarted',
            (data: any) => {
                this.messages.push('Job started. (JobId: ' + data.jobId + " Path: " + data.path + " )");
            });

        this.hubConnection.on('DuplicateFound',
            (data: any) => {
                this.messages.push('Duplicate found. (JobId: ' +
                    data.jobId +
                    ' | Duplicate: \"' +
                    data.duplicate.name +
                    '\" | Original: \"' +
                    data.original.name +
                    '\")');
            });
    }

    onSubmit() {
        this.http.post('/DuplicatesFinder/Job', JSON.stringify(this.path), { headers: this.headers }).subscribe(
            data => {
                console.log(data);
            });
    }
}
