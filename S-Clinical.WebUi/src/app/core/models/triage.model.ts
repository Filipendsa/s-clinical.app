import { PriorityLevelEnum } from "../enum/priority-level.enum";
import { SpecialtyTypeEnum } from "../enum/speciality-type.enum";

export interface CreateTriageModel {
  clinicalCareId: number;
  symptoms: string;
  bloodPressure: string;
  weight: number;
  height: number;
  speciality: SpecialtyTypeEnum;
  priority: PriorityLevelEnum;
}

export interface TriageModel {
  id: number;
  clinicalCareId: number;
  symptoms: string;
  bloodPressure: string;
  weight: number;
  height: number;
  speciality: SpecialtyTypeEnum;
  priority: PriorityLevelEnum;
}


export interface UpdateTriageModel {
  id: number;
  symptoms: string;
  bloodPressure: string;
  weight: number;
  height: number;
  speciality: SpecialtyTypeEnum;

}