export interface Hero {
    id: number;
    name: string;
    power: string; 
    imageUrl: string;
    description: string;
  }

  export interface HeroWithPowers{
    id: number;
    name: string;
    powers: string[]; 
    imageUrl: string;
    description: string;
  }