<script lang="ts">
  import { GamesClient, type BasicGameInfoDto } from "../../nlp-api";
  export let onGameUpdated: () => void;
  export let game: BasicGameInfoDto;
  
  let formChanged = false;
  const valueChanged = () => formChanged = true;

  const saveChanges = async () => {
    formChanged = false;
    const response = await new GamesClient().update(game);
    if(!response) {
      alert('Failed to update game!');
      return;
    }
    onGameUpdated();
  };
</script>

{#if game}
  <form>
    <div>
      <label class="label" for="gameName">
        <span class="label-text">Name</span>
      </label>
      <input type="text" id="gameName" class="input input-bordered w-full" bind:value={game.gameName} on:keyup={valueChanged}>
    </div>
    <div class="flex">
      <div class="flex-1 mr-2">
        <label class="label" for="caseLocation">
          <span class="label-text">Case Location</span>
        </label>
        <input type="text" id="caseLocation" class="input input-bordered w-full" bind:value={game.gameCaseLocation} on:keyup={valueChanged}>
      </div>
      <div class="flex-1 mr-2">
        <label class="label" for="gamePrice">
          <span class="label-text">Price</span>
        </label>
        <input type="number" id="gamePrice" class="input input-bordered w-full" bind:value={game.gamePrice} on:keyup={valueChanged}>
      </div>
      <div class="flex-1">
        <label class="label" for="rating">
          <span class="label-text">Rating</span>
        </label>
        <input type="number" min="0" class="input input-bordered w-full" max="10" id="rating" bind:value={game.gameRating} on:keyup={valueChanged}>
      </div>
    </div>
    <div class="flex">
      <div class="form-control flex-1">
        <label class="label cursor-pointer">
          <span class="label-text">Has Box</span> 
          <input type="checkbox" class="toggle" id="hasBox" bind:checked={game.hasGameBox} on:change={valueChanged} />
        </label>
      </div>
      <div class="form-control flex-1">
        <label class="label cursor-pointer">
          <span class="label-text">Has Protection</span> 
          <input type="checkbox" class="toggle" id="hasProtection" bind:checked={game.hasProtection} on:change={valueChanged} />
        </label>
      </div>
    </div>
    <button type="button" class="btn btn-primary w-full mt-2" disabled={!formChanged} on:click={saveChanges}>Save Changes</button>
  </form>
{/if}