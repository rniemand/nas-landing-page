<style>
	.description {
		border: 1px solid #3f3f3f;
		background-color: #161616;
		border-radius: 0.5em;
	}
	.details {
		display: flex;
		flex-wrap: wrap;
		justify-content: flex-start;
	}
	.details .entry {
		width: 30%;
		min-width: 200px;
		margin: 0.25em;
	}
</style>

<script lang="ts">
	import { Button } from 'sveltestrap';
	import type { HomeChoreDto } from '../../../nlp-api';
	import moment from 'moment';
	import ChoreIntervalDisplay from './ChoreIntervalDisplay.svelte';

	export let chore: HomeChoreDto;
	export let onEditChore: (chore: HomeChoreDto) => void;
	export let onCompleteChore: (chore: HomeChoreDto) => void;
	export let onDeleteChore: (chore: HomeChoreDto) => void;
</script>

{#if chore.choreDescription.length > 0}
	<div class="description p-2">{chore.choreDescription}</div>
{/if}

<div class="details mt-2">
	<div class="entry">
		<i class="bi bi-alarm" />
		{moment(chore.dateScheduled).fromNow()}
	</div>
	<div class="entry">
		<i class="bi bi-currency-dollar" />
		{chore.chorePoints} point(s)
	</div>
	<div class="entry">
		<i class="bi bi-geo" />
		{chore.floorName} / {chore.roomName}
	</div>
	<div class="entry">
		<i class="bi bi-journal-check" />
		{chore.completedCount}
	</div>
	<div class="entry">
		<i class="bi bi-calendar2-week-fill" />
		<ChoreIntervalDisplay {chore} />
	</div>
</div>

<div class="d-flex d-sm-block text-sm-end mt-2">
	<Button color="danger" on:click={() => onDeleteChore(chore)}>
		<i class="bi bi-trash3" />
		<span class="d-none d-sm-inline">Delete</span>
	</Button>
	<Button color="primary" class="flex-fill me-1" on:click={() => onEditChore(chore)}>
		<i class="bi bi-pencil-square" />
		<span class="d-none d-sm-inline">Edit</span>
	</Button>
	<!-- <Button color="warning" class="flex-fill me-1" disabled>
		<i class="bi bi-calendar-x-fill" />
		<span class="d-none d-sm-inline">Reschedule</span>
	</Button>
	<Button color="secondary" class="flex-fill me-1" disabled>
		<i class="bi bi-card-checklist" />
		<span class="d-none d-sm-inline">History</span>
	</Button> -->
	<Button color="success" class="flex-fill" on:click={() => onCompleteChore(chore)}>
		<i class="bi bi-check2-circle" />
		<span class="d-none d-sm-inline">Complete</span>
	</Button>
</div>
