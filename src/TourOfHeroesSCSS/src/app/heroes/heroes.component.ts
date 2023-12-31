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
    this.heroesSubscription = this.heroService.getHeroes().subscribe(heroes => (this.heroes = heroes));
  }

  add(name: string): void {
    name = name.trim();
    if (!name) {
      return;
    }
    this.heroesSubscription = this.heroService.addHero({ name: name } as Hero).subscribe(hero => {
      this.heroes.push(hero);
    });    
  }

  addHeroUser(heroId: number): void {
    console.log("HeroId");
    console.log(heroId);
    const userId = this.authService.userId(); 
    this.heroService.addHeroUser(userId, heroId).subscribe(() => {
    });
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
