import { CareStatusTypeEnum } from "../enum/care-status-type.enum";
import { PriorityLevelEnum } from "../enum/priority-level.enum";


export interface TriageInfo {
    id: number;
    symptoms: string;
    priority: PriorityLevelEnum;
}


export interface PatientInfo {
    id: number;
    name: string;
}

export interface ClinicalCareDetails {
    id: number;
    sequentialNumber: number;
    dateTimeArrival: Date;
    status: CareStatusTypeEnum;
    patient: PatientInfo;
    triage: TriageInfo;
}