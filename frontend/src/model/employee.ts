export interface IEmployee {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  jobTitle: string;
  dateOfJoining: Date;
  totalOfYearsInTheCompany: number;
}

export interface ICreateEmployee
  extends Omit<IEmployee, "id" | "totalOfYearsInTheCompany"> {}
