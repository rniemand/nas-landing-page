<script lang="ts">
	import { Accordion, AccordionItem, Col, Row } from 'sveltestrap';
	import AddChoreModal from './modals/AddChoreModal.svelte';
	import { ChoreClient, type HomeChoreDto } from '../../nlp-api';
	import ChoreInfoDisplay from './components/ChoreInfoDisplay.svelte';
	import EditChoreModal from './modals/EditChoreModal.svelte';
	import ChorePriorityIcon from './components/ChorePriorityIcon.svelte';
	import CompleteChoreModal from './modals/CompleteChoreModal.svelte';
	import NavigationCrumbs from '../../components/core/NavigationCrumbs.svelte';
	import NavigationCrumb from '../../components/core/NavigationCrumb.svelte';
	import { AppUrls } from '../../enums/AppUrls';

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
	const onChoreCompleted = () => refreshChores();

	const onEditChore = (chore: HomeChoreDto) => editModal?.editChore(chore);
	const onCompleteChore = (chore: HomeChoreDto) => completeModal?.completeChore(chore);

	refreshChores();
</script>

<NavigationCrumbs>
	<NavigationCrumb icon="bi-house-fill" url={AppUrls.Home} />
	<NavigationCrumb title="Chores" />
</NavigationCrumbs>

<Row class="mt-3">
	<Col class="text-end">
		<AddChoreModal {onChoreAdded} />
		<EditChoreModal bind:this={editModal} {onChoreUpdated} />
		<CompleteChoreModal bind:this={completeModal} {onChoreCompleted} />
	</Col>
</Row>

<Row class="mt-3">
	<Accordion class="rn-accordian">
		{#each chores as chore (chore.choreId)}
			<AccordionItem>
				<span class="m-0" slot="header">
					<ChorePriorityIcon priority={chore.priority} />
					{chore.choreName}
				</span>
				<ChoreInfoDisplay {chore} {onEditChore} {onCompleteChore} />
			</AccordionItem>
		{/each}
	</Accordion>
</Row>
