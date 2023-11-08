<script lang="ts">
	import { Button } from 'sveltestrap';
	import type { HomeDto } from '../../../nlp-api';

	export let home: HomeDto;
	export let onEdit: (home: HomeDto) => void;
	export let showFloors: () => void;
</script>

<div class="d-flex d-sm-block text-sm-end mt-2">
	<Button color="warning" class="flex-fill me-1" on:click={showFloors}>
		<i class="bi bi-search" />
		<span class="d-none d-sm-inline">Floors</span>
	</Button>
	<Button color="success" class="flex-fill" on:click={() => onEdit(home)}>
		<i class="bi bi-pencil-square" />
		<span class="d-none d-sm-inline">Edit</span>
	</Button>
</div>
