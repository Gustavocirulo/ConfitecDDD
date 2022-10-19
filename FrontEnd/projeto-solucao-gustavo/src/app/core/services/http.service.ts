import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { environment } from "src/environments/environment";
import { Observable } from "rxjs";

@Injectable(
  {
    providedIn: 'root'
  }
)

export class HttpService {

  private url_system: string = "";

  constructor(
    private http: HttpClient,
    private _router: Router,
  ) {
    this.url_system = environment.URL;

  }

  get(method: string): Observable<string> {
    return this.http.get<string>(this.url_system + method);
  }


  post(method: string, dataBody: object): Observable<any> {
    return this.http.post(this.url_system + method, dataBody);
  }

  put(method: string, dataBody: object = {}): Observable<any> {
    return this.http.put(this.url_system + method, dataBody);
  }

  delete(method: string, dataBody: object = {}): Observable<any> {
    return this.http.delete(this.url_system + method, {body: dataBody});
  }



}