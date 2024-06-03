import { Goal } from "./Goal";
import { CarePlan } from "./CarePlan";

export interface CarePlanGoal { 
    id?: number;
    goal?: Goal;
    carePlan?: CarePlan;
}