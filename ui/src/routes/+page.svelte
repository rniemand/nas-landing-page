<script lang="ts">
  import { WeatherForecast, WeatherForecastClient } from "../nlp-api";

  new WeatherForecastClient().get().then((forecast: WeatherForecast[]) => {
    console.log('x', forecast);
  });
</script>

<h1>Welcome to SvelteKit</h1>
<p>Visit <a href="https://kit.svelte.dev">kit.svelte.dev</a> to read the documentation</p>
