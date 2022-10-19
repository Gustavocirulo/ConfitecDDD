import { Injectable, Inject } from '@angular/core';
import { delay, map } from 'rxjs/operators';
import * as jwt_decode from 'jwt-decode';
import * as moment from 'moment';

import { environment } from '../../../environments/environment';
import { of, EMPTY } from 'rxjs';
import { HttpService } from './http.service';
import { usuarioDetailModel } from 'src/app/entidades/login/userDetail.model';
import { currentUserModel } from 'src/app/entidades/login/currentUser.model';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {

    constructor(private http: HttpService, private router: Router,
        @Inject('LOCALSTORAGE') private localStorage: Storage) {
    }

    login(email: string, password: string) {
        return this.http.post('CriarTokenIdentity', {email: email, senha: password, celular: ''})
    }

    saveToken(detail: any): void {
        this.logout();

        this.localStorage.setItem('currentUser', JSON.stringify({
            Token: detail['tokenJwt'],
            Email: detail['email'],
            Alias: detail['nome'],
            Id: detail['id'],
            Expiration: moment().add(60, 'minutes').toDate(),
            FullName: detail['nome'],
            IsAdmin: true
        }));
    }

    logout(): void {
        // clear token remove user from local storage to log user out
        this.localStorage.removeItem('currentUser');
    }

    getCurrentUser(): currentUserModel {
        let dataStored = this.localStorage.getItem('currentUser');

        if(dataStored != undefined) {
            return JSON.parse(dataStored) as currentUserModel;
        } 

        this.router.navigate(['auth/login']);
        return new currentUserModel();
    }

    passwordResetRequest(email: string) {
        return of(true).pipe(delay(1000));
    }

    changePassword(email: string, currentPwd: string, newPwd: string) {
        return of(true).pipe(delay(1000));
    }

    passwordReset(email: string, token: string, password: string, confirmPassword: string): any {
        return of(true).pipe(delay(1000));
    }
}
