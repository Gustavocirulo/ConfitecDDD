export interface usuarioModel {
    
    id: number;
    nome: string;
    sobrenome: string;
    email: string;
    dataNascimento: Date | null;
    escolaridade: number;
    // constructor(
    // public _Id: number,
    // public _Nome: string,
    // public _Sobrenome: string,
    // public _Email: string,
    // public _DataNascimento: Date | null,
    // public _Escolaridade: number
    // ) {
    //     this.Id = _Id; 
    //     this.Nome = _Nome,
    //     this.Sobrenome = _Sobrenome,
    //     this.Email = _Email,
    //     this.DataNascimento = _DataNascimento,
    //     this.Escolaridade = _Escolaridade
    // }
    
}