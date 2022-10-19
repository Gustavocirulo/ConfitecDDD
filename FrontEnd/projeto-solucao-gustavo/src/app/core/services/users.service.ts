import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { Observable, Observer } from 'rxjs';
import { usuarioModel } from 'src/app/entidades/usuarios/usuario.model';
import { HttpService } from './http.service';
import { NotificationService } from './notification.service';

@Injectable()
export class UserService{


    constructor(private injector: Injector, private servicehttp: HttpService, private notificationService: NotificationService) { }


    ListarPessoas(): Observable<Array<usuarioModel>> {

        return new Observable((_observer: Observer<Array<usuarioModel>>) => {
            this.servicehttp.get('ListarPessoas').subscribe({
                next: (res) => {

                    let jsonObj = JSON.parse(JSON.stringify(res));
                    let reponse = jsonObj as Array<usuarioModel>;

                    _observer.next(reponse);
                }, complete: () => {
                    _observer.complete();
                }, error: (err) => {
                    this.notificationService.openSnackBar('Falha ao Listar Usuários');
                    _observer.error(err);
                }
            })
        });

    }

    AdicionarPessoa(user: usuarioModel): Observable<Array<object>> {

        return new Observable((_observer: Observer<Array<usuarioModel>>) => {
            this.servicehttp.put('AdicionarPessoa', user).subscribe({
                next: (res) => {

                    let jsonObj = JSON.parse(JSON.stringify(res));
                    let reponse = jsonObj as Array<usuarioModel>;
                    this.notificationService.openSnackBar('Pessoa Adicionada com Sucesso');
                    _observer.next(reponse);
                    
                }, complete: () => {
                    _observer.complete();
                }, error: (err) => {
                    this.notificationService.openSnackBar('Falha ao adicionar Usuário');
                    _observer.error(err);
                }
            })
        });

    }


    AtualizarPessoa(user: usuarioModel): Observable<Array<object>> {
        return new Observable((_observer: Observer<Array<usuarioModel>>) => {
            this.servicehttp.post('AtualizarPessoa', user).subscribe({
                next: (res) => {

                    let jsonObj = JSON.parse(JSON.stringify(res));
                    let reponse = jsonObj as Array<usuarioModel>;
                    
                    this.notificationService.openSnackBar('Pessoa Atualizada com Sucesso');
                    _observer.next(reponse);
                    
                }, complete: () => {
                    _observer.complete();
                }, error: (err) => {
                    this.notificationService.openSnackBar('Falha ao Atualizar Usuário');
                    _observer.error(err);
                }
            })
        });
    }

    ExcluirPessoa(user: usuarioModel): Observable<Array<object>> {
        return new Observable((_observer: Observer<Array<usuarioModel>>) => {
            this.servicehttp.delete('ExcluirPessoa', user).subscribe({
                next: (res) => {

                    let jsonObj = JSON.parse(JSON.stringify(res));
                    let reponse = jsonObj as Array<usuarioModel>;
                    
                    this.notificationService.openSnackBar('Pessoa Removida com Sucesso');
                    _observer.next(reponse);
                    
                }, complete: () => {
                    _observer.complete();
                }, error: (err) => {
                    this.notificationService.openSnackBar('Falha ao Excluir Usuário');
                    _observer.error(err);
                }
            })
        });
    }
}
