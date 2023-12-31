import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Hero } from '../hero';
import { Power } from '../power';
import { MessageService } from './message.service';


@Injectable({ providedIn: 'root' })
export class HeroService {

  private heroesUrl = 'https://localhost:44346/api/Heroes';  

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient,
    private messageService: MessageService) { }


    private powersUrl = 'https://localhost:44346/api/powers';

    getPowers(): Observable<Power[]> {
      return this.http.get<Power[]>(this.powersUrl)
        .pipe(
          tap(_ => this.log('fetched powers')),
          catchError(this.handleError<Power[]>('getPowers', []))
        );
    }


    savePower(power: Power): Observable<Power> {
      return this.http.post<Power>(this.powersUrl, power)
        .pipe(
          tap(_ => this.log('saved power')),
          catchError(this.handleError<Power>('savePower'))
        );
    }
    updatePower(power: Power): Observable<Power> {
      const url = `${this.powersUrl}/${power.id}`; 
      return this.http.put<Power>(url, power)
        .pipe(
          tap(_ => this.log('updated power')),
          catchError(this.handleError<Power>('updatePower'))
        );
    }

    deletePower(powerId: number): Observable<void> {
      const url = `${this.powersUrl}/${powerId}`;
      return this.http.delete<void>(url)
        .pipe(
          tap(_ => this.log('deleted power')),
          catchError(this.handleError<void>('deletePower'))
        );
    }


    getHeroPowers(heroId: number): Observable<Power[]> {
      const url = `${this.heroesUrl}/${heroId}/powers`; // Adjust the URL to match your API
      return this.http.get<Power[]>(url)
        .pipe(
          tap(_ => this.log('fetched hero powers')),
          catchError(this.handleError<Power[]>('getHeroPowers', []))
        );
    }

  /** GET heroes from the server */
  getHeroes(): Observable<Hero[]> {
    return this.http.get<Hero[]>(this.heroesUrl)
      .pipe(
        tap(_ => this.log('fetched heroes')),
        catchError(this.handleError<Hero[]>('getHeroes', []))
      );
  }

  getHeroesUser(userId: number): Observable<Hero[]> {
    const url = `https://localhost:44346/api/Favorites/${userId}`;
    return this.http.get<Hero[]>(url)
      .pipe(
        tap(_ => this.log('fetched heroes')),
        catchError(this.handleError<Hero[]>('getHeroes', []))
      );
  }

  addHeroUser(userId: number, heroId: number): Observable<void> {
    const url =  `https://localhost:44346/api/Favorites/${userId}?heroId=${heroId}`;
    return this.http.post<void>(url, this.httpOptions).pipe(
      tap(() => this.log(`Added heroUser: userId=${userId}, heroId=${heroId}`)),
      catchError(this.handleError<void>('addHeroUser'))
    );
  }


   /** GET hero by id. Return `undefined` when id not found */
   getHeroNo404<Data>(id: number): Observable<Hero> {
    const url = `${this.heroesUrl}/?id=${id}`;
    return this.http.get<Hero[]>(url)
      .pipe(
        map(heroes => heroes[0]), // returns a {0|1} element array
        tap(h => {
          const outcome = h ? 'fetched' : 'did not find';
          this.log(`${outcome} hero id=${id}`);
        }),
        catchError(this.handleError<Hero>(`getHero id=${id}`))
      );
  }

  /** GET hero by id. Will 404 if id not found */
  getHero(id: number): Observable<Hero> {
    const url = `${this.heroesUrl}/${id}`;
    return this.http.get<Hero>(url).pipe(
      tap(_ => this.log(`fetched hero id=${id}`)),
      catchError(this.handleError<Hero>(`getHero id=${id}`))
    );
  }



  /*   getHero(id: number): Observable<{ hero: Hero, powers: string[] }> {
    const url = `${this.heroesUrl}/${id}`;
    return this.http.get<{ hero: Hero, powers: string[] }>(url).pipe(
      tap(_ => this.log(`fetched hero id=${id}`)),
      catchError(this.handleError<{ hero: Hero, powers: string[] }>(`getHero id=${id}`))
    );
  }*/

  /* GET heroes whose name contains search term */
  searchHeroes(term: string): Observable<Hero[]> {
    if (!term.trim()) {
      return of([]);
    } 
    term = term.toLowerCase(); 
    return this.http.get<Hero[]>(this.heroesUrl).pipe(
      map(heroes => heroes.filter(hero => hero.name.toLowerCase().includes(term))), 
      tap(filteredHeroes => {
        if (filteredHeroes.length) {
          this.log(`found heroes matching "${term}"`);
        } else {
          this.log(`no heroes matching "${term}"`);
        }
      }),
      catchError(this.handleError<Hero[]>('searchHeroes', []))
    );
  }
  

  //////// Save methods //////////

  /** POST: add a new hero to the server */
  addHero(hero: Hero): Observable<Hero> {
    
    return this.http.post<Hero>(this.heroesUrl, hero, this.httpOptions).pipe(
      tap((newHero: Hero) => this.log(`added hero w/ id=${newHero.id}`)),
      catchError(this.handleError<Hero>('addHero'))
    );
  }
  

  /** DELETE: delete the hero from the server */
  deleteHero(id: number): Observable<Hero> {
    const url = `${this.heroesUrl}/${id}`;

    return this.http.delete<Hero>(url, this.httpOptions).pipe(
      tap(_ => this.log(`deleted hero id=${id}`)),
      catchError(this.handleError<Hero>('deleteHero'))
    );
  }

  /** PUT: update the hero on the server */
  updateHero(hero: Hero): Observable<any> {
    const url =  `${this.heroesUrl}/${hero.id}`;
    return this.http.put(url, hero, this.httpOptions).pipe(
      tap(_ => this.log(`updated hero id=${hero.id}`)),
      catchError(this.handleError<any>('updateHero'))
    );
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   *
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  /** Log a HeroService message with the MessageService */
  private log(message: string) {
    this.messageService.add(`HeroService: ${message}`);
  }
}