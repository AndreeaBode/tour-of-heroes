import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Location } from '@angular/common';
import { Hero } from '../hero';
import { HeroService } from '../services/hero.service';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-heroes',
  templateUrl: './heroes.component.html',
  styleUrls: ['./heroes.component.scss']
})
export class HeroesComponent implements OnInit, OnDestroy {
  heroes: Hero[] = [];
  hero: Hero | undefined;
  private heroesSubscription: Subscription | undefined;
  showAddForm = false;
  isImageClicked = false;

  constructor(private heroService: HeroService,
    private location: Location,
    private authService: AuthService) { }

    ngOnInit(): void {
      this.getHeroes();
      document.addEventListener('click', this.handleClickOutsideImage.bind(this));
    }
    
  ngOnDestroy(): void {
    this.unsubscribe();
  }

  getHeroes(): void {
    this.heroesSubscription = this.heroService.getHeroes().subscribe(heroes => (this.heroes = heroes));
  }

  add(name: string): void {
    name = name.trim();
    if (!name) {
      return;
    }
    this.heroesSubscription = this.heroService.addHero({ name } as Hero).subscribe(hero => {
      this.heroes.push(hero);
    });
  }



  selectedHero: any;

  showHeroDetails(hero: any): void {
    this.selectedHero = hero;
  }

  
  goBack(): void {
    this.location.back();
    this.isImageClicked = false;
  }

 save(): void {
  if (this.hero) {
    this.heroesSubscription = this.heroService.updateHero(this.hero).subscribe(() => this.goBack());
  }
}


  delete(hero: Hero): void {
    const heroIdToDelete = hero.id;
    this.heroes = this.heroes.filter(h => h !== hero);

    this.unsubscribe(); 

    this.heroService.deleteHero(heroIdToDelete).subscribe(
      () => {  
      },
      error => {
        console.error('Error deleting hero:', error);
        this.getHeroes();
      }
    );
  }


  private unsubscribe(): void {
    if (this.heroesSubscription) {
      this.heroesSubscription.unsubscribe();
    }
  }
  
  toggleAddForm(): void {
    this.showAddForm = !this.showAddForm;
  }
  handleClickOutsideImage(event: MouseEvent): void {
    if (!this.isImageClicked) {
      return;
    }
  
    const target = event.target as HTMLElement;
    const image = document.querySelector('.selected-hero-image') as HTMLElement;
  
    if (image && !image.contains(target)) {
      this.isImageClicked = false;
    }
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }
}
