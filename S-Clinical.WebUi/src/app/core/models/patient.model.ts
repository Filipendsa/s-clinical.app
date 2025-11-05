import { GenderType } from "../enum/gender-type.enum";

export interface PatientModel {
    id: number;
    name: string;
    phoneNumber: string;
    email: string;
    gender: GenderType;
}

export interface CreatePatientModel {
    name: string;
    phoneNumber: string;
    email: string;
    gender: GenderType;
}

export interface UpdatePatientModel {
    id: number;
    name: string;
    phoneNumber: string;
    email: string;
    gender: GenderType;
}