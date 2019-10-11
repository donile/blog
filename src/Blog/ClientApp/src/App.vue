<template>
  <div id="app">
    <div class="container-fluid">
      <div class="row">
        <div class="col-4 offset-4">
          <h1 class="text-center">markdonile.com</h1>
        </div>
      </div>
    </div>

    <home-page/>

    <div class="container-fluid">
      <div class="row">
        <div class="col-4 offset-4">
          <span>
            <p class="text-center">
            <a asp-controller="About">about</a>
            ::
            <a asp-controller="Archives">archives</a>
            </p>
          </span>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import HomePage from './components/HomePage.vue'

export default {
  name: 'app',
  components: {
    'home-page': HomePage
  }
}
</script>