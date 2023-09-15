import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Location } from '@angular/common';
import { Hero } from '../hero';
import { HeroService } from '../services/hero.service';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-favorite-heroes',
  templateUrl: './favorite-heroes.component.html',
  styleUrls: ['./favorite-heroes.component.scss']
})
export class FavoriteHeroesComponent {
  heroes: Hero[] = [];
  hero: Hero | undefined;
  private heroesSubscription: Subscription | undefined;
  showAddForm = false;
  isImageClicked = false;
  selectedHero: any;
  userRole: string = "User";

  constructor(private heroService: HeroService,
    public authService: AuthService) { }

    ngOnInit(): void {
      this.getHeroes();
      this.userRole = this.authService.userRole();
      console.log(this.userRole);
    }
    
  ngOnDestroy(): void {
    this.unsubscribe();
  }

  getHeroes(): void {
    const userId = this.authService.userId();
    this.heroesSubscription = this.heroService.getHeroesUser(userId).subscribe(heroes => (this.heroes = heroes));
  }

 

  showHeroDetails(hero: Hero): void {
    this.selectedHero = hero;
    console.log(this.selectedHero);
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

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  scrollToTop(): void {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }
}