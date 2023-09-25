<style>
	.game {
		border: 1px solid #4f4f4f;
		width: 180px;
		margin: 0.5em;
		border-radius: 0.5em;
	}
	.game .image img {
		width: 100%;
		border-radius: 0.5em;
		border-top-left-radius: 0.5em;
	}
</style>

<script lang="ts">
	import type { GameDto } from '../../nlp-api';

	export let game: GameDto;
</script>

<div class="game shadow px-0">
	<div class="image">
		<img
			src={`/api/games/game-cover/${game.platformName?.toLowerCase()}/${game.gameID}`}
			alt={game.gameName} />
	</div>
	<div class="title text-center text-truncate px-1 py-1">
		{game.gameName}
	</div>
</div>
