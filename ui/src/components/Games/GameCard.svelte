<style>
  .card {
    width: 200px;
    border: 1px solid #8b8b8b;
    margin: 6px;
    border-radius: 6px;
    background-color: #252525;
  }
  h2 { text-align: center; font-size: 1.2em; padding: 3px 2px; }
  .image { margin: 3px; }
  img.cover {
    width: 100%;
    border-radius: 5px;
  }
</style>

<script lang="ts">
  import type { BasicGameInfoDto } from "../../nlp-api";

  export let game: BasicGameInfoDto;
</script>

<div class="card">
  <h2 class="ellipsis">{game.gameName}</h2>
  <div class="image">
    <!-- svelte-ignore a11y-missing-attribute -->
    <img class="cover" src={`api/images/game/cover/${game.platformName}/${game.gameID}`} />
  </div>
</div>