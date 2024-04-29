export class UserLoginDto {
    EmailUser: string;
    PasswordUser: string;

    constructor(EmailUser: string, PasswordUser: string)
    {
        this.EmailUser = EmailUser;
        this.PasswordUser = PasswordUser;
    }
}