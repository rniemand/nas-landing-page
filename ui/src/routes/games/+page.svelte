<script lang="ts">
	import GamePlatforms from './GamePlatforms.svelte';
	import type { GamePlatformDto } from '../../nlp-api';
	import PlatformGames from './PlatformGames.svelte';

	let platform: GamePlatformDto | undefined = undefined;
</script>

<GamePlatforms bind:value={platform} />
<PlatformGames {platform} />
