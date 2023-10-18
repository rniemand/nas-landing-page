<script lang="ts">
	import { Accordion, AccordionItem, Col, Row } from 'sveltestrap';
	import AddChoreModal from './modals/AddChoreModal.svelte';
	import { ChoreClient, type HomeChoreDto } from '../../nlp-api';
	import ChoreInfoDisplay from './components/ChoreInfoDisplay.svelte';
	import EditChoreModal from './modals/EditChoreModal.svelte';
	import ChorePriorityIcon from './components/ChorePriorityIcon.svelte';
	import CompleteChoreModal from './modals/CompleteChoreModal.svelte';

	let loading: boolean = true;
	let chores: HomeChoreDto[] = [];
	let editModal: EditChoreModal;
	let completeModal: CompleteChoreModal;

	const refreshChores = async () => {
		loading = true;
		chores = await new ChoreClient().getChores();
		loading = false;
	};

	const onChoreAdded = () => refreshChores();
	const onChoreUpdated = () => refreshChores();

	const onEditChore = (chore: HomeChoreDto) => editModal?.editChore(chore);
	const onCompleteChore = (chore: HomeChoreDto) => completeModal?.completeChore(chore);

	refreshChores();
</script>

<Row>
	<Col class="text-end">
		<AddChoreModal {onChoreAdded} />
		<EditChoreModal bind:this={editModal} {onChoreUpdated} />
		<CompleteChoreModal bind:this={completeModal} />
	</Col>
</Row>
<Row>
	<Col>
		<Accordion class="mt-1 rn-accordian">
			{#each chores as chore}
				<AccordionItem>
					<span class="m-0" slot="header"
						><ChorePriorityIcon priority={chore.priority} /> {chore.choreName}</span>
					<ChoreInfoDisplay {chore} {onEditChore} {onCompleteChore} />
				</AccordionItem>
			{/each}
		</Accordion>
	</Col>
</Row>
