import { Component, OnInit, OnDestroy } from '@angular/core';
import { HeroService } from '../services/hero.service';
import { Hero } from '../hero';
import { Subject, takeUntil } from 'rxjs';
import { Location } from '@angular/common';

@Component({
  selector: 'app-hero-form',
  templateUrl: './hero-form.component.html',
  styleUrls: ['./hero-form.component.scss'],
})
export class HeroFormComponent implements OnDestroy {
  selectedPower: string[] = [];
  heroPower: string = '';
  powers: string[] = [
    'Invisibility',
    'Super Speed',
    'Time Travel',
    'Mind Control',
    'Immortality',
    'Animal Communication',
  ];

  private destroy$ = new Subject<void>();

  constructor(private heroService: HeroService, private location: Location) {}

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
  goBack(): void {
    this.location.back();
  }

  addHero(
    name: string,
    powers: string[],
    imageUrl: string,
    description: string
  ): void {
    name = name.trim();
    imageUrl = imageUrl.trim();
    description = description.trim();

    if (!name || powers.length === 0) {
      return;
    }

    const newHero: Hero = {
      id: 0,
      name: name,
      powers: powers,
      imageUrl: imageUrl,
      description: description,
    };

    this.heroService
      .addHero(newHero)
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => this.location.back());
  }
}
