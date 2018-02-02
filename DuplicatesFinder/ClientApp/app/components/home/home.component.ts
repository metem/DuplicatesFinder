import { Component } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    path: string = '';
    private headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    constructor(private http: HttpClient) {
    }

    onSubmit() {
        this.http.post('http://localhost:60692/DuplicatesFinder/Job', JSON.stringify(this.path), { headers: this.headers }).subscribe(data => {
            console.log(data);
        });
    }
}

