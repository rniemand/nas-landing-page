<script lang="ts">
  import { GamesClient, type BasicGameInfoDto } from "../../nlp-api";
  export let onGameUpdated: () => void;
  export let game: BasicGameInfoDto;
  
  let formChanged = false;
  const valueChanged = () => formChanged = true;
  const saveChanges = () => {
    formChanged = false;
    new GamesClient().update(game).then((_response: BasicGameInfoDto) => {
      if(!_response) { alert('Failed to update game!'); return; }
      onGameUpdated();
    });
  };
</script>

{#if game}
  <form class="row g-3">
    <div class="col-md-12">
      <label for="gameName" class="form-label">Name</label>
      <input type="text" id="gameName" class="form-control" bind:value={game.gameName} on:keyup={valueChanged}>
    </div>
    <div class="col-md-3">
      <label for="caseLocation" class="form-label">Case Location</label>
      <input type="text" id="caseLocation" class="form-control" bind:value={game.gameCaseLocation} on:keyup={valueChanged}>
    </div>
    <div class="col-md-3">
      <label for="gamePrice" class="form-label">Price</label>
      <input type="number" id="gamePrice" class="form-control" bind:value={game.gamePrice} on:keyup={valueChanged}>
    </div>
    <div class="col-3">
      <label for="rating" class="form-label">Rating</label>
      <input type="number" min="0" class="form-control" max="10" id="rating" bind:value={game.gameRating} on:keyup={valueChanged}>
    </div>
    <div class="d-flex">
      <div class="col-6">
        <div class="form-check form-switch form-check-inline">
          <input type="checkbox" class="form-check-input" id="hasBox" bind:checked={game.hasGameBox} on:change={valueChanged}>
          <label class="form-check-label" for="hasBox">Has Box</label>
        </div>
      </div>
      <div class="col-6">
        <div class="form-check form-switch form-check-inline">
          <input type="checkbox" class="form-check-input" id="hasProtection" bind:checked={game.hasProtection} on:change={valueChanged}>
          <label class="form-check-label" for="hasProtection">Has Protection</label>
        </div>
      </div>
    </div>
    <button type="button" class="btn btn-primary" disabled={!formChanged} on:click={saveChanges}>Save Changes</button>
  </form>
{/if}