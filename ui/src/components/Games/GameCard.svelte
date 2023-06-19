<style>
  .card {
    width: 200px;
    border: 1px solid #3f3f3f;
    margin: 6px;
    border-radius: 6px;
    background-color: #161616;
    box-shadow: 3px 3px 3px 0px rgb(18 18 18);
  }
  h2 { text-align: center; font-size: 1.2em; padding: 3px 2px; }
  .image { margin: 3px; position: relative; text-align: center; }
  img.cover { width: auto; border-radius: 5px; max-height: 200px; }
  .sold { opacity: 0.5; background-color: #37010e; }
  .price { background-color: rgba(0,0,0,.61); border-radius: 3px; color: #00ff1f; padding: 3px; position: absolute; right: 5px; top: 5px; }
</style>

<script lang="ts">
  import type { BasicGameInfoDto } from "../../nlp-api";

  export let game: BasicGameInfoDto;
</script>

<div class="card" class:sold={game.gameSold}>
  <h2 class="ellipsis">{game.gameName}</h2>
  <div class="image">
    {#if game.gamePrice > 0}<div class="price">{game.gamePrice}</div>{/if}
    <!-- svelte-ignore a11y-missing-attribute -->
    <img class="cover" src={`api/images/game/cover/${game.platformName}/${game.gameID}`} />
  </div>
</div>