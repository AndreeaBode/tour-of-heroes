import { Component, OnInit, OnDestroy } from '@angular/core';
import { HeroService } from '../services/hero.service';
import { Hero, HeroWithPowers } from '../hero';
import { Subject, takeUntil } from 'rxjs';
import { Location } from '@angular/common';
import { Power } from '../power';

@Component({
  selector: 'app-hero-form',
  templateUrl: './hero-form.component.html',
  styleUrls: ['./hero-form.component.scss'],
})
export class HeroFormComponent implements OnDestroy {
  selectedPower: string[] = [];
  heroPower: string = '';
  powers: Power[] = [];

  private destroy$ = new Subject<void>();

  constructor(private heroService: HeroService, private location: Location) {}

  ngOnInit():void{
    this.fetchPowers();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
  goBack(): void {
    this.location.back();
  }

  addHero(
    name: string,
    imageUrl: string,
    description: string
  ): void {
    name = name.trim();
    imageUrl = imageUrl.trim();
    description = description.trim();

    const newHero: Hero = {
      id: 0,
      name: name,
      power: this.selectedPower.join(","),
      imageUrl: imageUrl,
      description: description,
    };
    
//console.log(newHero);

     this.heroService
       .addHero(newHero)
       .pipe(takeUntil(this.destroy$))
       .subscribe(() => this.location.back());
  }

  fetchPowers(): void {
    this.heroService
      .getPowers()
      .pipe(takeUntil(this.destroy$))
      .subscribe((powers) => (this.powers = powers));
  }
}
