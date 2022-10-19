import { CdkTable, CdkTableDataSourceInput } from '@angular/cdk/table';
import { AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Title } from '@angular/platform-browser';

import { NGXLogger } from 'ngx-logger';
import { NotificationService } from 'src/app/core/services/notification.service';
import { UserService } from 'src/app/core/services/users.service';
import { usuarioModel } from 'src/app/entidades/usuarios/usuario.model';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit, AfterViewInit {

  dataSource = new MatTableDataSource<usuarioModel>();
  @ViewChild(MatTable) table!: MatTable<usuarioModel>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  displayedColumns: string[] = ['id', 'nome', 'sobrenome', 'email', 'dataNascimento', 'escolaridade', 'acao'];

  matcher = new MyErrorStateMatcher();
  maxDate!: Date;
  addUserForm!: FormGroup;

  constructor(
    private userService: UserService,
    private fb: FormBuilder,
    private notificationService: NotificationService,
    private titleService: Title
  ) {


    this.maxDate = new Date();
    this.addUserForm = this.createFormGroup();


  }

  ngOnInit() {
    this.titleService.setTitle('angular-material-template - Users');
    this.getUserList();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  private getUserList(): void {
    let teste = this.userService.ListarPessoas().subscribe({
      next: (data: usuarioModel[]) => {
        this.dataSource.data = data;
      },
      complete: () => { this.table.renderRows(); }
    });
  }

  createFormGroup(): FormGroup {
    let nomeFormControl = new FormControl('', Validators.compose([Validators.required, Validators.maxLength(100)]));
    let sobrenomeFormControl = new FormControl('', Validators.compose([Validators.required, Validators.maxLength(100)]));
    let emailFormControl = new FormControl('', Validators.compose([Validators.required, Validators.pattern('^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$'), Validators.maxLength(100)]));
    let dataNascimentoFormControl = new FormControl(new Date(), [Validators.required]);
    let escolaridadeFormControl = new FormControl('', [Validators.required]);

    return this.fb.group({
      idFormControl: 0,
      nomeFormControl: nomeFormControl,
      sobrenomeFormControl: sobrenomeFormControl,
      emailFormControl: emailFormControl,
      dataNascimentoFormControl: dataNascimentoFormControl,
      escolaridadeFormControl: escolaridadeFormControl,
    })

  }

  onSubmit(): void {

    if (this.addUserForm.valid) {
      var id_value = this.addUserForm.get('idFormControl')?.value;
      var pessoa: usuarioModel = {
        id: id_value == null ? 0 : id_value,
        nome: this.addUserForm.get('nomeFormControl')?.value,
        sobrenome: this.addUserForm.get('sobrenomeFormControl')?.value,
        email: this.addUserForm.get('emailFormControl')?.value,
        dataNascimento: this.addUserForm.get('dataNascimentoFormControl')?.value,
        escolaridade: this.addUserForm.get('escolaridadeFormControl')?.value
      };


      if (pessoa.id) {
        this.onEditarPessoa(pessoa)
      } else {
        this.onSalvarPessoa(pessoa)
      }

    }
    else {
      this.notificationService.openSnackBar('Corrija os erros do formulário antes de prosseguir.');
    }
  }

  onSalvarPessoa(element: usuarioModel): void {
    this.userService.AdicionarPessoa(element).subscribe({
      complete: () => {
        this.getUserList();
        this.addUserForm.reset();
      }
    })
  }

  onExcluirPessoa(element: usuarioModel): void {
    this.userService.ExcluirPessoa(element).subscribe({
      complete: () => {
        this.getUserList();
      }
    });
  }

  onPreencherCamposEditar(element: usuarioModel): void {

    this.addUserForm.get('idFormControl')?.setValue(element.id)
    this.addUserForm.get('nomeFormControl')?.setValue(element.nome)
    this.addUserForm.get('sobrenomeFormControl')?.setValue(element.sobrenome)
    this.addUserForm.get('emailFormControl')?.setValue(element.email)
    this.addUserForm.get('dataNascimentoFormControl')?.setValue(element.dataNascimento)
    this.addUserForm.get('escolaridadeFormControl')?.setValue(element.escolaridade)
  }

  onEditarPessoa(element: usuarioModel): void {


    this.userService.AtualizarPessoa(element).subscribe({
      complete: () => {
        this.getUserList();
        this.addUserForm.reset();
      }
    });
  }


  public user_validation_messages = {
    'nome': [
      { type: 'required', message: 'Nome é ', bold: 'obrigatório' },
      { type: 'maxlength', message: 'O limite é <b>100</b> caracteres' },
    ],
    'sobrenome': [
      { type: 'required', message: 'Sobrenome é ', bold: 'obrigatório' },
      { type: 'maxlength', message: 'O limite é 100 caracteres' },
    ],
    'email': [
      { type: 'required', message: 'Email é ', bold: 'obrigatório' },
      { type: 'maxlength', message: 'O limite é 100 caracteres' },
      { type: 'pattern', message: 'Insira uma email válido' },
    ],
    'dataNascimento': [
      { type: 'required', message: 'Data de Nascimento é ', bold: 'obrigatório' },
      { type: 'matDatepickerMax', message: 'Insira uma data menor que a atual' },
    ],
    'escolaridade': [
      { type: 'required', message: 'Escolaridade é ', bold: 'obrigatório' }
    ]
  }

  public tipo_Escolaridade =
    [
      { nome: 'Infantil', valor: 1 },
      { nome: 'Fundamental', valor: 2 },
      { nome: 'Medio', valor: 3 },
      { nome: 'Superior', valor: 4 }
    ]

}

/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

export const MY_FORMATS = {
  parse: {
    dateInput: 'DD/MM/YYYY'
  },
  display: {
    dateInput: 'DD/MM/YYYY',
    monthYearLabel: 'YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'YYYY'
  }
};