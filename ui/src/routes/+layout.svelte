<script>
  import NavBar from "../components/NavBar.svelte";
  import 'bootstrap/dist/css/bootstrap.min.css';
  import 'bootstrap-icons/font/bootstrap-icons.min.css';
  import TopNavigation from "../components/TopNavigation.svelte";
</script>

<style>
  .app-wrapper {
    padding-top: 45px;
  }
</style>

<TopNavigation />
<!-- <NavBar /> -->
<div class="app-wrapper">
  <slot />
</div>