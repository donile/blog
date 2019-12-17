<template>
    <div>
        <h1>AdminHome.vue</h1>
        <ul>
            <li>Blog Posts</li>
        </ul>
    </div>
</template>

<script>
import AuthenticationService from '../services/authentication.js';

export default {
    name: "AdminHome",
    async beforeRouteEnter(to, from, next){
        
        const authService = new AuthenticationService();

        const authenticated = authService.isAuthenticated();
        
        if(!authenticated){
            next( { name: 'UserSignIn' } );
        }
        else{
            next( );
        }
    }
}
</script>