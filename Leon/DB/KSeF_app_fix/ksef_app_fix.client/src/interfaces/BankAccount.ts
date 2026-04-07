

export interface BankAccount {
    id: number;
    fullNumber?: string | null;
    swift?: string | null;
    bankName?: string | null;
    description?: string | null;
    isBankOwnAccount: number;
}
