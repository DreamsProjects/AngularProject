import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { BlogComponent } from './components/blog/blog.component';
import { FoodComponent } from './components/food/food.component';
import { CompetitionComponent } from './components/competition/competition.component';
import { UserComponent } from './components/user/user.component';
import { CreateComponent } from './components/create/create.component';
import { CartComponent } from './components/cart/cart.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
		BlogComponent,
		FoodComponent,
		HomeComponent,
		UserComponent,
		CompetitionComponent,
		CreateComponent,
		CartComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
			{ path: 'food', component: FoodComponent },
			{ path: 'blog', component: BlogComponent },
			{ path: 'user', component: UserComponent },
			{ path: 'competition', component: CompetitionComponent },
			{ path: 'create', component: CreateComponent },
			{ path: 'cart', component: CartComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
