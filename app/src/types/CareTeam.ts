import { Member } from "./Member";
import { Person } from "./Person";

export interface CareTeam { 
    member: Member;
    people: Person[];
}
