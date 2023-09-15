import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Subscription } from 'rxjs';

import { Hero } from '../hero';
import {Power} from '../power';
import { HeroService } from '../services/hero.service';

@Component({
  selector: 'app-hero-detail',
  templateUrl: './hero-detail.component.html',
  styleUrls: ['./hero-detail.component.scss']
})
export class HeroDetailComponent implements OnInit, OnDestroy {
  hero: Hero | undefined;
  private heroSubscription: Subscription | undefined;
  powers: string[] = [];
  selectedPowers: string[] = []


  constructor(
    private route: ActivatedRoute,
    private heroService: HeroService,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.getHero();
    this.heroService
      .getPowers()
      .subscribe(powers => {
          this.powers = [];
          powers.forEach((power) => {
            this.powers.push(power.name);
          })
      });
  }
  ngOnDestroy(): void {
    this.unsubscribe();
  }

  getHero(): void {
    const id = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
    this.heroSubscription = this.heroService.getHero(id).subscribe(hero => {
      if (hero && hero.power) { 
        this.selectedPowers = hero.power.split(', ');
        this.hero = hero;
      }
    });
  }

  goBack(): void {
    this.location.back();
  }

  save(): void {
    if (this.hero) {
      const selectedPowersString = this.selectedPowers.join(', ');
      this.hero.power = selectedPowersString;
      this.heroSubscription = this.heroService.updateHero(this.hero).subscribe(() => this.goBack());
    }
  }

  private unsubscribe(): void {
    if (this.heroSubscription) {
      this.heroSubscription.unsubscribe();
    }
  }
}
