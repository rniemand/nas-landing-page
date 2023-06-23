<style>
    h2 { font-size: 1.6em; text-align: center; margin-bottom: 12px; }
    .form .row {
    display: flex;
    justify-content: space-evenly;
    margin-bottom: 12px;
  }
  .form .field label {
    font-weight: bold;
    padding-top: 2px;
    margin-right: 4px;
  }
  .form .field {
    flex: auto;
    display: flex;
  }
  .form .field input {
    flex: auto;
    margin-right: 6px;
    padding: 2px;
    border-radius: 4px;
    border: 1px solid #b9b5b5;
  }
</style>

<script lang="ts">
	import { GamesClient, type BasicGameInfoDto } from "../../nlp-api";

    export let game: BasicGameInfoDto;
    let formChanged = false;

    const valueChanged = () => formChanged = true;

    const saveChanges = () => {
        formChanged = false;
        new GamesClient().update(game)
            .then((_response: BasicGameInfoDto) => {
                if(!_response) {
                    alert('Failed to update game!');
                    return;
                }
            });
    };
</script>

{#if game}
    <h2>{game.gameName}</h2>
    <div class="form">
        <div class="row">
          <div class="field">
            <label for="location">Case</label>
            <input type="text" id="location" bind:value={game.gameCaseLocation} on:keyup={valueChanged}>
          </div>
          <div class="field">
            <label for="name">Name</label>
            <input type="text" id="name" bind:value={game.gameName} on:keyup={valueChanged}>
          </div>
        </div>
      
        <div class="row">
          <div class="field">
            <label for="price">Price</label>
            <input type="number" id="price" bind:value={game.gamePrice} on:keyup={valueChanged}>
          </div>
          <div class="field">
            <label for="rating">Rating</label>
            <input type="text" id="rating" bind:value={game.gameRating} on:keyup={valueChanged}>
          </div>
        </div>
      
        <div class="row">
            <div class="field">
                <label for="hasBox">Has Box</label>
                <input type="checkbox" id="hasBox" bind:checked={game.hasGameBox} on:change={valueChanged}>
              </div>
              <div class="field">
                <label for="hasProtection">Has Protection</label>
                <input type="checkbox" id="hasProtection" bind:checked={game.hasProtection} on:change={valueChanged}>
              </div>
        </div>
      
        <div class="row">
          <button disabled={!formChanged} on:click={saveChanges}>Save Changes</button>
        </div>
      </div>
{/if}
