import { Routes } from "@angular/router";
import { Dashboard } from "./dashboard/dashboard";
import { ClinicalCareComponent } from "./clinical-care/component/clinical-care.component";
import { TriageComponent } from "./triage/component/triage.component";
import { PatientComponent } from "./patient/component/patient.component";

export const MAIN_ROUTES: Routes = [
    {
        path: '',
        title: 'S-Clinical Dashboard',
        component: Dashboard
    },
        {
        path: 'atendimento',
        title: 'Atendimento',
        component: ClinicalCareComponent
    },
        {
        path: 'triagem',
        title: 'Triagem',
        component: TriageComponent
    },
        {
        path: 'paciente',
        title: 'Paciente',
        component: PatientComponent
    },
];