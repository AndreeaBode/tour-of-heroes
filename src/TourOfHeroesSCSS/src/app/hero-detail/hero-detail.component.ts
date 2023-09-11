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
  powers: Power[] = [];


  constructor(
    private route: ActivatedRoute,
    private heroService: HeroService,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.getHero();
  }
  ngOnDestroy(): void {
    this.unsubscribe();
  }

  getHero(): void {
    const id = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
    this.heroSubscription = this.heroService.getHero(id).subscribe(hero => (this.hero = hero));
  }

  getHeroPowers(): void {
    const id = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
    this.heroService.getHeroPowers(id).subscribe(powers => (this.powers = powers)); 
  }
  
  goBack(): void {
    this.location.back();
  }

  save(): void {
    if (this.hero) {
      this.heroSubscription = this.heroService.updateHero(this.hero).subscribe(() => this.goBack());
    }
  }

  private unsubscribe(): void {
    if (this.heroSubscription) {
      this.heroSubscription.unsubscribe();
    }
  }
}
