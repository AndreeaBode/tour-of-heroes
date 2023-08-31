import { Component, OnInit, OnDestroy} from '@angular/core';
import { HeroService } from '../services/hero.service';
import { Hero } from '../hero';
import { Subject, takeUntil } from 'rxjs';
import { Location } from '@angular/common';

@Component({
  selector: 'app-hero-form',
  templateUrl: './hero-form.component.html',
  styleUrls: ['./hero-form.component.scss']
})
export class HeroFormComponent  implements OnDestroy {
  private destroy$ = new Subject<void>();
  constructor(private heroService: HeroService,
    private location: Location) {}


  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
  goBack(): void {
    this.location.back();
  }

  addHero(name: string, 
    power: string, 
    imageUrl: string, 
    description: string): void {
      
      name = name.trim();
      power = power.trim();
      imageUrl = imageUrl.trim();
      description = description.trim();

      if (!name || !power) {
        return;
      }

      this.heroService.addHero({name, power, imageUrl, description} as Hero)
      .pipe(takeUntil(this.destroy$))
        .subscribe(() => this.location.back())
      }
}



