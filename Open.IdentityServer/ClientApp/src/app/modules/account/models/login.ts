export interface Login {
    userName: string
    password: string
    rememberMe: boolean
    ldap: boolean

    returnUrl: string
}
