import { Member } from "./Member";
import { Problem } from "./Problem";
import { Goal } from "./Goal";
import { CarePlanGoal } from "./CarePlanGoal";
import { Intervention } from "./Intervention";
import { Barrier } from "./Barrier";

export interface CarePlan { 
    id: number;
    member: Member;
    problem: Problem;
    //goal: Goal;
    carePlanGoals: CarePlanGoal[];
    interventions: Intervention[];
    barriers: Barrier[];
    carePlanName: string;
}