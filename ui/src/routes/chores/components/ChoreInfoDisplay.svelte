<script lang="ts">
	import { Button } from 'sveltestrap';
	import type { HomeChoreDto } from '../../../nlp-api';
	import moment from 'moment';
	import ChoreIntervalDisplay from './ChoreIntervalDisplay.svelte';
	import DetailsContainer from '../../../components/common/DetailsContainer.svelte';
	import DetailsContainerEntry from '../../../components/common/DetailsContainerEntry.svelte';

	export let chore: HomeChoreDto;
	export let onEditChore: (chore: HomeChoreDto) => void;
	export let onCompleteChore: (chore: HomeChoreDto) => void;
	export let onDeleteChore: (chore: HomeChoreDto) => void;
</script>

{#if chore.choreDescription.length > 0}
	<div class="description p-2">{chore.choreDescription}</div>
{/if}

<DetailsContainer>
	<DetailsContainerEntry icon="bi-alarm" value={moment(chore.dateScheduled).fromNow()} />
	<DetailsContainerEntry icon="bi-currency-dollar" value={`${chore.chorePoints} point(s)`} />
	<DetailsContainerEntry icon="bi-geo" value={`${chore.floorName} / ${chore.roomName}`} />
	<DetailsContainerEntry icon="bi-journal-check" value={chore.completedCount} />
	<DetailsContainerEntry icon="bi-calendar2-week-fill">
		<ChoreIntervalDisplay {chore} />
	</DetailsContainerEntry>
</DetailsContainer>

<div class="d-flex d-sm-block text-sm-end mt-2">
	<Button color="danger" class="flex-fill me-1" on:click={() => onDeleteChore(chore)}>
		<i class="bi bi-trash3" />
		<span class="d-none d-sm-inline">Delete</span>
	</Button>
	<Button color="primary" class="flex-fill me-1" on:click={() => onEditChore(chore)}>
		<i class="bi bi-pencil-square" />
		<span class="d-none d-sm-inline">Edit</span>
	</Button>
	<Button color="success" class="flex-fill" on:click={() => onCompleteChore(chore)}>
		<i class="bi bi-check2-circle" />
		<span class="d-none d-sm-inline">Complete</span>
	</Button>
</div>

<style>
	.description {
		border: 1px solid #3f3f3f;
		background-color: #161616;
		border-radius: 0.5em;
	}
</style>
